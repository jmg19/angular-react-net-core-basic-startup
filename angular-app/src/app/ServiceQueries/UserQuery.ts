import { SearchRule } from './SearchRule';
import { OrderingRule } from './OrderingRule';

export class UserQuery {
  public search_rules: SearchRule[];
  public ordering_rules: OrderingRule[];
}
