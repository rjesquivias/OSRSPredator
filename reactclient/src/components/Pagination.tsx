import { Pagination } from 'semantic-ui-react'

interface Props {
    page: number
    totalPages: number
    updatePage: (page: number | string | undefined) => void
}

const PaginationCompact = ({page, totalPages, updatePage}: Props) => (
  <Pagination
    boundaryRange={0}
    defaultActivePage={1}
    ellipsisItem={null}
    firstItem={null}
    lastItem={null}
    siblingRange={1}
    totalPages={totalPages}
    onPageChange={(event, data) => updatePage(data.activePage)}
  />
)

export default PaginationCompact