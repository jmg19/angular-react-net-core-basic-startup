import { SearchRule } from './SearchRule';
import { OrderingRule } from './OrderingRule';

export interface UserQuery {
  search_rules: SearchRule[];
  ordering_rules: OrderingRule[];
}
