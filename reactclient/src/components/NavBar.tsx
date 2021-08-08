import axios from "axios";
import { observer } from "mobx-react-lite";
import { Menu } from "semantic-ui-react";
import { useStore } from "../stores/store";

const NavBar = () => {

    const { itemStore } = useStore();

    function getItemDetails(item: any) {
        return axios.all([
            axios.get(`https://localhost:5001/api/ItemPriceSnapshot/${item.snapshotId}`),
            axios.get(`https://localhost:5001/api/ItemDetail/${item.detailsId}`)
        ]).then(responses => {
            item.mostRecentSnapshot = responses[0].data;
            item.itemDetails = responses[1].data;
            item.id = 0;
        })
    }

    const handleItemClick = async (e: any, { name }: any) => {
        itemStore.setNavState(name);
        itemStore.setCheckedItems([]);
        if(name === 'All Items')
        {
            axios.get(`https://localhost:5001/api/v1/Analytics?pageSize=100&page=1`).then(response => {
                console.log(response.data);
                itemStore.setSimpleItemAnalysisList(response.data);
            });
        } else if(name === 'Watchlist') {
            axios.get(`https://localhost:5001/api/v1/Analytics/Watchlist?pageSize=100&page=1`).then((response) => {
                var watchlist = response.data;
                const promises: any[] = [];
                watchlist.forEach((item: any) => {
                    promises.push(getItemDetails(item))
                })

                Promise.all(promises).then(() => {
                    console.log(watchlist)
                    itemStore.setSimpleItemAnalysisList(watchlist)
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
                    active={itemStore.navState === 'All Items'}
                    onClick={handleItemClick}
                />
                <Menu.Item
                    name='Watchlist'
                    active={itemStore.navState === 'Watchlist'}
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

export default observer(NavBar);
