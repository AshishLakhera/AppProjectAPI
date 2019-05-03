import { Component, OnInit } from '@angular/core';
import { client } from '../ClientFactory/ClientFactory';

@Component({
  selector: 'login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  ngOnInit() {
    client.LoadCSS('login');
  }
}
