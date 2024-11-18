

import { Component,OnInit , inject} from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,
    CommonModule, 
    MatToolbarModule, 
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.sass'
})

export class LoginComponent implements OnInit {
  
  
  private readonly _router =inject(Router);
  private readonly _authService =inject(AuthService);
  mensaje: string = '';
  username: string = '';
  password: string = '';


  onLogin() {
    console.log('Ingresando al sistema...');
    this._authService.login(this.username, this.password).subscribe({
      next: (res) =>  this._router.navigate(['/home']),
      error: (error) => {
      if (error.status === 401) {
       
        this.mensaje = 'Usuario o contraseña incorrecto';
      } else {
        this.mensaje = 'Ha ocurrido un error, por favor intente nuevamente';
        console.log(error);
      }
    }
  });
  }

  ngOnInit(): void {    
  }
}
