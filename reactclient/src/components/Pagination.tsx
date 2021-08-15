import { observer } from 'mobx-react-lite';
import { Pagination } from 'semantic-ui-react'
import { useStore } from '../stores/store'

const PaginationCompact = () => {

  const { itemStore } = useStore();

  return (
    <Pagination
      boundaryRange={0}
      defaultActivePage={1}
      ellipsisItem={null}
      firstItem={null}
      lastItem={null}
      siblingRange={1}
      totalPages={itemStore.totalPages}
      onPageChange={(event, data) => itemStore.loadAllItems(data.activePage)}
    />
  )
}

export default observer(PaginationCompact)