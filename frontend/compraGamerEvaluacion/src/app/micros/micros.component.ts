import { Component, OnInit } from '@angular/core';
import { MicrosService, MicroReadDto, MicroCreateDto, MicroUpdateDto } from '../services/micros.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-micros',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './micros.component.html',
  styleUrl: './micros.component.css',
})
export class MicrosComponent implements OnInit {
  micros: MicroReadDto[] = [];
  form: FormGroup;
  editingId: number | null = null;

  constructor(private service: MicrosService, private fb: FormBuilder) {
    this.form = this.fb.group({
      patente: [''],
      marcaModelo: ['']
    });
  }

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll(): void {
    this.service.getAll().subscribe(data => {
      this.micros = data;
    });
  }

  edit(m: MicroReadDto): void {
    this.editingId = m.id;
    this.form.setValue({ patente: m.patente, marcaModelo: m.marcaModelo });
  }

  cancelEdit(): void {
    this.editingId = null;
    this.form.reset();
  }

  save(): void {
    const dto = {
      patente: this.form.value.patente,
      marcaModelo: this.form.value.marcaModelo
    } as MicroCreateDto;

    if (this.editingId === null) {
      this.service.create(dto).subscribe(_ => {
        this.loadAll();
        this.form.reset();
      });
    } else {
      this.service.update(this.editingId, dto as MicroUpdateDto).subscribe(_ => {
        this.loadAll();
        this.cancelEdit();
      });
    }
  }

  remove(id: number): void {
    if (confirm('¿Eliminar este micro?')) {
      this.service.delete(id).subscribe(_ => {
        this.loadAll();
      });
    }
  }
}