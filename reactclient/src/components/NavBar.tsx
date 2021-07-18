import axios from "axios";
import { useState } from "react";
import { Menu } from "semantic-ui-react";

interface Props {
    setSimpleItemAnalysisList: (itemList: any[]) => void
}

const NavBar = ({setSimpleItemAnalysisList} : Props) => {
    const [activeItem, setActiveItem] = useState("All Items");
    const [watchlistResponse, setWatchlistResponse] = useState<any>(null);

    const updateItem = async(item: any) => {
        const promises = [];
        promises.push(axios.get(`https://localhost:5001/api/ItemPriceSnapshot/${item.snapshotId}`));
        promises.push(axios.get(`https://localhost:5001/api/ItemDetail/${item.detailsId}`));

        Promise.all(promises).then((responses) => {
            item.mostRecentSnapshot = responses[0].data;
            item.itemDetails = responses[1].data;
            item.id = 0;
        })
    }

    const handleItemClick = async (e: any, { name }: any) => {
        console.log(name);
        setActiveItem(name);
        if(name === 'All Items')
        {
            axios.get(`https://localhost:5001/api/v1/Analytics?pageSize=100&page=1`).then(response => {
                console.log(response.data);
                setSimpleItemAnalysisList(response.data);
            });
        } else if(name === 'Watchlist') {
            axios.get(`https://localhost:5001/api/v1/Analytics/Watchlist?pageSize=100&page=1`).then((response) => {
                var watchlist = response.data;
                const promises: any[] = [];
                watchlist.forEach((item: any) => {
                    promises.push(updateItem(item));
                })

                Promise.all(promises).then((responses) => {
                    console.log(watchlist)
                    setWatchlistResponse(watchlist)
                    setSimpleItemAnalysisList(watchlistResponse)
                })
            })
        }
    }

    return (
        <div className="ui secondary pointing menu massive">
            <div className="item">
                <img style={{width: '30px', height: 'auto'}} src="/logo512.png"/>
                <div style={{margin: '10px'}}>OSRSPredator</div>
            </div>

            <Menu pointing secondary className="right menu">
                <Menu.Item
                    name='All Items'
                    active={activeItem === 'All Items'}
                    onClick={handleItemClick}
                />
                <Menu.Item
                    name='Watchlist'
                    active={activeItem === 'Watchlist'}
                    onClick={handleItemClick}
                />
            </Menu>
            
            <div className="right menu">
                <div className="item">
                    <i className="large icon bell outline"/>
                </div>
                <div className="item">
                    <i className="large icon mail outline"/>
                </div>
                <div className="item">
                    <i className="large icon home"/>
                </div>
            </div>
        </div>
            
    )
}

export default NavBar
