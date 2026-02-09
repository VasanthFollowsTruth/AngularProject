import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ContactService } from '../../services/contact.service';
import { Contact } from '../../models/contact.model';
import { PagedResult } from '../../models/paged-result.model';
import { ContactFormComponent } from '../../components/contact-form/contact-form.component';

@Component({
  selector: 'app-contacts',
  standalone: true,
  imports: [CommonModule, ContactFormComponent],
  templateUrl: './contacts.component.html'
})
export class ContactsComponent implements OnInit {

  showAddModal = false;
  showEditModal = false;
  showDeleteModal = false;

  highlightedId: number | null = null;

  selectedContact!: Contact;

  totalCount = 0;
  contacts: Contact[] = [];

  // Paging
  page = 1;
  pageSize = 10;
  totalPages = 0;

  // Sorting
  sortBy = '';
  order: 'asc' | 'desc' = 'asc';

  loading = false;

  constructor(
    private contactService: ContactService
  ) { }

  ngOnInit(): void {
    const savedId = localStorage.getItem('highlightedContactId');

    if (savedId) {
      this.highlightedId = parseInt(savedId, 10);
    }

    this.loadContacts();
  }

  loadContacts(): void {

    this.loading = true;

    this.contactService.getPaged(
      this.page,
      this.pageSize,
      this.sortBy || undefined,
      this.order
    )
      .subscribe({

        next: (res: PagedResult<Contact>) => {

          this.contacts = res.items;

          this.totalPages = res.totalPages;
          this.totalCount = res.totalCount;

          this.loading = false;
        },

        error: () => {
          this.loading = false;
        }
      });
  }

  // Sorting handler
  sort(column: string): void {

    if (this.sortBy === column) {
      this.order = this.order === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortBy = column;
      this.order = 'asc';
    }

    this.page = 1;
    this.loadContacts();
  }

  // Pagination
  changePage(newPage: number): void {

    if (newPage < 1 || newPage > this.totalPages) {
      return;
    }

    this.page = newPage;
    this.loadContacts();
  }

  get startRecord(): number {
    return (this.page - 1) * this.pageSize + 1;
  }

  get endRecord(): number {

    const end = this.page * this.pageSize;

    return end > this.totalCount
      ? this.totalCount
      : end;
  }

  private emptyContact(): Contact {
    return {
      firstName: '',
      lastName: '',
      email: '',
      phoneNumber: '',
      address: '',
      city: '',
      state: '',
      country: '',
      postalCode: ''
    };
  }

  openAdd(): void {
    this.selectedContact = this.emptyContact();
    this.showAddModal = true;
  }

  openEdit(contact: Contact): void {
    this.selectedContact = { ...contact };
    this.showEditModal = true;
  }

  openDelete(contact: Contact): void {
    this.selectedContact = contact;
    this.showDeleteModal = true;
  }

  closeModals(): void {
    this.showAddModal = false;
    this.showEditModal = false;
    this.showDeleteModal = false;
  }

  addContact(contact: Contact): void {

    this.contactService.create(contact).subscribe({
      next: (created) => {

        this.highlightedId = created.id!;

        console.log('Setting highlight to:', this.highlightedId);

        localStorage.setItem(
          'highlightedContactId',
          created.id!.toString()
        );

        this.sortBy = '';
        this.order = 'asc';

        this.page = 1;

        this.loadContacts();

        this.closeModals();
      }
    });
  }

  updateContact(contact: Contact): void {

    this.contactService
      .update(contact.id!, contact)
      .subscribe({
        next: () => {
          this.closeModals();
          this.loadContacts();
        }
      });
  }

  deleteContact(): void {

    const deletedId = this.selectedContact.id!;

    this.contactService
      .delete(deletedId)
      .subscribe({

        next: () => {

          if (this.highlightedId === deletedId) {

            this.highlightedId = null;
            localStorage.removeItem('highlightedContactId');
          }

          this.closeModals();
          this.loadContacts();
        }
      });
  }

  getSortIcon(column: string): string {

    if (this.sortBy !== column) {
      return '⇅'; // Sorting not activated
    }

    return this.order === 'asc' ? '▲' : '▼';
  }
}
