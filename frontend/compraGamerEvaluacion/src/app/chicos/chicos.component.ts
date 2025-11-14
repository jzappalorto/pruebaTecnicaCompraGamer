import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ChicosService, Chico } from '../services/chicos.service';
import { MicrosService, Micro } from '../services/micros.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';


@Component({
  selector: 'app-chicos',
  standalone: true,
  templateUrl: './chicos.component.html',
  styleUrls: ['./chicos.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class ChicosComponent implements OnInit {
  chicos: Chico[] = [];
  micros: Micro[] = [];

  editingId: number | null = null;

form: any;

  constructor(
    private fb: FormBuilder,
    private service: ChicosService,
    private microService: MicrosService
  ) {}

ngOnInit(): void {
  this.form = this.fb.group({
    dni: ['', Validators.required],
    nombre: ['', Validators.required],
    microId: [null, Validators.required],
  });

  this.loadAll();
}

getMicroPatente(id: number | null): string {
  if (id === null) return '—';
  const micro = this.micros.find(m => m.id === id);
  return micro ? micro.patente : '—';
}

  loadAll(): void {
    this.service.getAll().subscribe({
      next: (data) => (this.chicos = data),
    });

    this.microService.getAll().subscribe({
      next: (data) => (this.micros = data),
    });
  }

  save(): void {
    if (this.form.invalid) {
      alert('Completa todos los campos. El micro es obligatorio.');
      return;
    }

    const dto = this.form.value as any;

    if (this.editingId === null) {
      this.service.create(dto).subscribe({
        next: () => this.loadAll(),
      });
    } else {
      this.service.update(this.editingId, dto).subscribe({
        next: () => {
          this.loadAll();
          this.editingId = null;
        },
      });
    }

    this.form.reset();
  }

  edit(chico: Chico): void {
    this.editingId = chico.id;
    this.form.patchValue({
      dni: chico.dni,
      nombre: chico.nombre,
      microId: chico.microId
    });
  }

  delete(id: number): void {
    if (!confirm('¿Seguro?')) return;

    this.service.delete(id).subscribe({
      next: () => this.loadAll(),
    });
  }
}
