import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Contact } from '../../models/contact.model';

@Component({
  selector: 'app-contact-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contact-form.component.html'
})
export class ContactFormComponent {

  @Input() contact!: Contact;
  @Input() isEdit = false;

  @Output() save = new EventEmitter<Contact>();
  @Output() cancel = new EventEmitter<void>();

  onSubmit(): void {
    this.save.emit(this.contact);
  }

  onCancel(): void {
    this.cancel.emit();
  }
}
