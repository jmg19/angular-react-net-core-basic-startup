export interface PagingRule {
  page: number;
  pageSize: number;
}

export interface SearchRule
{
  condition: string;
  pagingRule: PagingRule;
}
