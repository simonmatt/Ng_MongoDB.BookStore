import { Injectable } from '@angular/core';
import { State, Action, StateContext, Selector } from '@ngxs/store';
import { BookAction, GetBooks, CreateUpdateBook, DeleteBook } from './book.actions';
import { BookDto } from '../models';
import { PagedResultDto } from '@abp/ng.core';
import { BookService } from '../services';
import { tap } from 'rxjs/operators';

export class BookStateModel {
  public book: PagedResultDto<BookDto>;
}

const defaults = {
  items: []
};

@State<BookStateModel>({
  name: 'BookState',
  defaults: { book: {} } as BookStateModel
})
@Injectable()
export class BookState {
  @Selector()
  static getBooks(state: BookStateModel) {
    return state.book.items || [];
  }

  constructor(private bookService: BookService) { }

  @Action(GetBooks)
  get(ctx: StateContext<BookStateModel>) {
    return this.bookService.getListByInput().pipe(
      tap((booksResponse) => {
        ctx.patchState({
          book: booksResponse
        });
      })
    );
  }

  @Action(CreateUpdateBook)
  save(ctx: StateContext<BookStateModel>, action: CreateUpdateBook) {
    if (action.id) {
      return this.bookService.updateByIdAndInput(action.payload, action.id);
    } else {
      return this.bookService.createByInput(action.payload);
    }
  }

  @Action(DeleteBook)
  delete(ctx: StateContext<BookStateModel>, action: DeleteBook) {
    return this.bookService.deleteById(action.id);
  }
}
