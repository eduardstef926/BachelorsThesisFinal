import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule, MatDateSelectionModel } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { CoreService } from './services/core.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { LoginPageComponent } from './login-page/login-page.component';
import { MatTableModule } from '@angular/material/table';
import { MainPageComponent } from './main-page/main-page.component';
import { AuthService } from './services/auth.service';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { LocationPageComponent } from './locations-page/locations-page.component';
import { SignUpPageComponent } from './login-page/sign-up-page/sign-up-page.component';
import { ForgotPasswordComponent } from './login-page/forgot-password/forgot-password.component';
import { ModifyPasswordComponent } from './login-page/modify-password/modify-password.component';
import { EmployeePageComponent } from './employee-page/employee-page.component';
import { EmployeeService } from './services/employee.service';
import { MatPaginatorModule } from '@angular/material/paginator';
import { LocalStorageService } from './services/localstorage.service';
import { ChatPageComponent } from './chat-page/chat-page.component';
import { SymptomPageComponent } from './symptom-page/symptom-page.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { SecondPhaseComponent } from './second-phase/second-phase.component';

@NgModule({
  declarations: [
    AppComponent, 
    LoginPageComponent, 
    MainPageComponent, 
    SignUpPageComponent, 
    ForgotPasswordComponent, 
    LocationPageComponent, 
    ModifyPasswordComponent, 
    EmployeePageComponent, 
    ChatPageComponent, 
    SymptomPageComponent, 
    SecondPhaseComponent, 
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatSidenavModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    MatSelectModule,
    MatTableModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatInputModule,
    BrowserAnimationsModule,
    MatListModule,
    ReactiveFormsModule,
    FormsModule,
    MatCardModule,
    MatSnackBarModule,
    MatPaginatorModule,
    MatCheckboxModule
  ],
  providers: [CoreService, AuthService, EmployeeService, LocalStorageService],
  bootstrap: [AppComponent],
})
export class AppModule {}
