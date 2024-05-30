import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  http = inject(HttpClient);

  data: any = [];

  ngOnInit(): void {
      this.http.get('https://localhost:7043/api/Khoa')
      .subscribe((data: any) => {
        console.log(data);
        this.data = data;

      });
  }
}
