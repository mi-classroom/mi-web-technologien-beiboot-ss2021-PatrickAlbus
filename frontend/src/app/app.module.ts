import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { FileBrowsingComponent } from './file-browsing/file-browsing.component';
import { TreeViewComponent } from './file-browsing/tree-view/tree-view.component';

import { environment } from '../environments/environment'
import { BASE_PATH } from '../api/variables';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FileBrowsingComponent,
    TreeViewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }
    ])
  ],
  providers: [{ provide: BASE_PATH, useValue: environment.API_BASE_PATH }],
  bootstrap: [AppComponent]
})
export class AppModule { }
