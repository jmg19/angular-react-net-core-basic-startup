export class PagingRule {
  public page: number;
  public page_size: number;
}

export class SearchRule
{
  public condition: string;
  public paging_rule: PagingRule;
}
