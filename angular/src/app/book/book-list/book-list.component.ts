import { Component, OnInit } from '@angular/core';
import { Select, Store } from '@ngxs/store';
import { BookState } from '../state/book.state';
import { Observable } from 'rxjs';
import { BookDto, BookType, CreateUpdateBookDto } from '../models';
import { GetBooks, CreateUpdateBook, DeleteBook } from '../state/book.actions';
import { finalize } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BookService } from '../services';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {
  @Select(BookState.getBooks)
  books$: Observable<BookDto[]>;

  bookType = BookType;

  bookTypeArr = Object.keys(BookType).filter(
    (booktype) => typeof this.bookType[booktype] === 'number'
  );

  loading = false;

  isModalOpen = false;

  form: FormGroup;

  selectedBook = {} as BookDto;

  constructor(
    private store: Store,
    private fb: FormBuilder,
    private bookService: BookService,
    private confirmation: ConfirmationService) { }

  ngOnInit(): void {
    this.get();
  }

  get() {
    this.loading = true;
    this.store.dispatch(new GetBooks())
      .pipe(finalize(() => (this.loading = false)))
      .subscribe(() => { });
  }

  createBook() {
    this.selectedBook = {} as BookDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editBook(id: string) {
    this.bookService.getById(id)
      .subscribe(book => {
        console.log(book);
        this.selectedBook = book;
        this.buildForm();
        this.isModalOpen = true;
      });
  }

  buildForm() {
    let dt = {};
    const publishDate = this.selectedBook.publishDate;
    if (publishDate) {
      const dd = new Date(publishDate);
      const year = dd.getFullYear();
      const month = dd.getMonth() + 1;
      const day = dd.getDate();
      dt = {
        year,
        month,
        day
      };
    }
    this.form = this.fb.group({
      name: [this.selectedBook.name || '', Validators.required],
      type: [this.selectedBook.type || null, Validators.required],
      publishDate: [
        this.selectedBook.publishDate
          ? dt
          : null,
        Validators.required],
      price: [this.selectedBook.price || null, Validators.required]
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const val = this.form.value;

    const publish = val.publishDate;

    console.log(publish);

    const dt = new Date(publish.year, publish.month-1, publish.day);

    console.log(JSON.stringify(dt));

    let newBook: CreateUpdateBookDto = {
      name: val.name,
      type: val.type,
      price: val.price,
      publishDate: dt.toDateString()
    };

    console.log(publish);

    this.store.dispatch(new CreateUpdateBook(newBook,this.selectedBook.id))
      .subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.get();
      });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure')
      .subscribe(status => {
        if (status === Confirmation.Status.confirm) {
          this.store.dispatch(new DeleteBook(id))
            .subscribe(() => this.get());
        }
      });
  }

}
