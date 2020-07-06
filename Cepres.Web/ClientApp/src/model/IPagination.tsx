export interface IPagination<T> {
    pageIndex: number;
    pageSize: number;
    count: number;
    pageCount: number;
    next: boolean;
    previous: boolean;
    sort: string,
    data: T;
}