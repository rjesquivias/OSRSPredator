import axios from "axios";
import { useState } from "react";
import { Menu } from "semantic-ui-react";

interface Props {
    setSimpleItemAnalysisList: (itemList: any[]) => void
}

const NavBar = ({setSimpleItemAnalysisList} : Props) => {
    const [activeItem, setActiveItem] = useState("All Items");

    const handleItemClick = (e: any, { name }: any) => {
        console.log(name);
        setActiveItem(name);
        if(name === 'All Items')
        {
            axios.get(`https://localhost:5001/api/v1/Analytics?pageSize=100&page=1`).then(response => {
                console.log(response);
                setSimpleItemAnalysisList(response.data);
            });
        } else if(name === 'Watchlist') {
            axios.get(`https://localhost:5001/api/v1/Analytics/Watchlist?pageSize=100&page=1`).then(response => {
                console.log(response);
                setSimpleItemAnalysisList(response.data);
            });
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
