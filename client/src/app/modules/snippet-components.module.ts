import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCommentComponent } from '../components/snippets/post-comment/post-comment.component';
import { StickySidebarRComponent } from '../components/snippets/sticky-sidebar-r/sticky-sidebar-r.component';
import { PhotoUploadComponent } from '../components/snippets/photo-upload/photo-upload.component';
import { FileUploadModule } from 'ng2-file-upload';
import { PhotoUpload2Component } from '../components/snippets/photo-upload2/photo-upload.component';
import { MatRangeSliderComponent } from '../components/snippets/mat-range-slider/mat-range-slider.component';
import {MatSliderModule} from '@angular/material/slider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDialogComponent } from '../components/snippets/mat-dialog/mat-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { SharedModule } from './shared.module';
import { MatInputModule } from '@angular/material/input';



@NgModule({

  imports: [
    CommonModule,
    FileUploadModule,
    MatSliderModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    SharedModule,
    MatInputModule
  ],

  declarations: [
    PostCommentComponent,
    StickySidebarRComponent,
    PhotoUploadComponent,
    PhotoUpload2Component,
    MatRangeSliderComponent,
    MatDialogComponent
  ],

  exports:[
    PostCommentComponent,
    StickySidebarRComponent,
    PhotoUploadComponent,
    PhotoUpload2Component,
    FileUploadModule,
    MatRangeSliderComponent,
    MatDialogComponent
  ]
})
export class SnippetComponentsModule { }
