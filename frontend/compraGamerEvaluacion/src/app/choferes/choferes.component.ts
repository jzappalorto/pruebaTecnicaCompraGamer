import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { ChoferesService, ChoferReadDto, ChoferCreateDto, ChoferUpdateDto } from '../services/choferes.service';
import { MicrosService, MicroReadDto } from '../services/micros.service';


@Component({
  standalone: true,
  selector: 'app-choferes',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './choferes.component.html',
  styleUrl: './choferes.component.css',
})
export class ChoferesComponent implements OnInit {
  showAssignModal = false;
  selectedChofer: ChoferReadDto | null = null;
  selectedMicroId: number | null = null;

  choferes: ChoferReadDto[] = [];
  micros: MicroReadDto[] = [];

  form: FormGroup;
  editingId: number | null = null;

  constructor(
    private service: ChoferesService,
    private microsService: MicrosService,
    private fb: FormBuilder
  ) {
    this.form = this.fb.group({
      dni: [''],
      nombre: [''],
      microId: ['']
    });
  }

  getMicroNombre(id: number | null): string {
    if (id === null) return '—';
  const micro = this.micros.find(m => m.id === id);
  return micro ? micro.patente : '—';
}

  openAssignModal(chofer: ChoferReadDto): void {
  this.selectedChofer = chofer;
  this.selectedMicroId = chofer.microId ?? null;
  this.showAssignModal = true;
}

closeAssignModal(): void {
  this.showAssignModal = false;
  this.selectedChofer = null;
  this.selectedMicroId = null;
}

confirmAssign(): void {
  if (!this.selectedChofer) return;

  if (this.selectedMicroId === null) {
    alert("Seleccioná un micro válido.");
    return;
  }

  this.service.assign(this.selectedChofer.id, this.selectedMicroId).subscribe({
    next: () => {
      this.loadAll();
      this.closeAssignModal();
    },
    error: (err) => {
      console.error(err);
      alert("Asignación faallida.");
    }
  });
}


  ngOnInit(): void {
    this.loadAll();
  }

  loadAll(): void {
    this.service.getAll().subscribe(data => this.choferes = data);
    this.microsService.getAll().subscribe(data => this.micros = data);
  }

  edit(c: ChoferReadDto): void {
    this.editingId = c.id;
    this.form.setValue({
      dni: c.dni,
      nombre: c.nombre,
      microId: c.microId ?? ''
    });
  }

  cancelEdit(): void {
    this.editingId = null;
    this.form.reset();
  }

  save(): void {
    const dto = {
      dni: this.form.value.dni,
      nombre: this.form.value.nombre,
      microId: Number(this.form.value.microId)
    } as ChoferCreateDto;

    if (this.editingId === null) {
      this.service.create(dto).subscribe(_ => {
        this.loadAll();
        this.form.reset();
      });
    } else {
      this.service.update(this.editingId, dto as ChoferUpdateDto).subscribe(_ => {
        this.loadAll();
        this.cancelEdit();
      });
    }
  }

  remove(id: number): void {
    if (confirm('¿Eliminar este chofer?')) {
      this.service.delete(id).subscribe(_ => this.loadAll());
    }
  }

  assign(c: ChoferReadDto): void {
    const microId = Number(prompt("ID del micro para asignar:"));
    if (!microId) return;

    this.service.assign(c.id, microId).subscribe({
      next: () => this.loadAll(),
      error: () => alert("Asignación fallida. Verificá IDs.")
    });
  }
}
