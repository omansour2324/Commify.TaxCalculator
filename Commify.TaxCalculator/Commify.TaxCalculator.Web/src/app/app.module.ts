import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { TaxCalculatorComponent } from './tax/tax-calculator.component';

@NgModule({
  declarations: [
    AppComponent,
    TaxCalculatorComponent
  ],
  imports: [
    BrowserModule, FormsModule, HttpClientModule, AppRoutingModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
