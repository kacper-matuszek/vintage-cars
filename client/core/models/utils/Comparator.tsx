import { Order } from "../../../components/base/table-list/table-head/TableHeadComponent";

function descendingComparator<T>(a: T, b: T, orderBy: keyof T) {
    if (b[orderBy] < a[orderBy]) {
      return -1;
    }
    if (b[orderBy] > a[orderBy]) {
      return 1;
    }
    return 0;
  }

function getComparator<Key extends keyof any>(
    order: Order,
    orderBy: Key,
  ): (a: { [key in Key]: any }, b: { [key in Key]: any }) => number {
    return order === 'desc'
      ? (a, b) => descendingComparator(a, b, orderBy)
      : (a, b) => -descendingComparator(a, b, orderBy);
};

export default getComparator;