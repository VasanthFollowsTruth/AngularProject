import { Component, OnInit } from '@angular/core';
import { ContactService } from '../../services/contact.service';
import { Contact } from '../../models/contact.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-test-api',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './test-api.component.html'
})
export class TestApiComponent implements OnInit {

  contacts: Contact[] = [];

  constructor(private contactService: ContactService) { }

  ngOnInit(): void {

    this.contactService.getAll().subscribe({
      next: (data) => {
        this.contacts = data;
        console.log('Contacts:', data);
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}
