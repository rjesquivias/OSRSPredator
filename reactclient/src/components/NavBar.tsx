import { observer } from "mobx-react-lite";
import { Menu } from "semantic-ui-react";
import { useStore } from "../stores/store";
import { NavLink } from "react-router-dom";

const NavBar = () => {

    const { itemStore } = useStore();

    const handleNavClick = async (e: any, { name }: any) => {
        itemStore.setNavState(name);
        itemStore.setCheckedItems([]);
    }

    return (
        <div className="ui secondary pointing menu massive">
            <div className="item">
                <img style={{width: '30px', height: 'auto'}} src="/logo512.png"/>
                <div style={{margin: '10px'}}>OSRSPredator</div>
            </div>

            <Menu pointing secondary className="right menu">
                <Menu.Item as={NavLink} to={"/itemDashboard"}
                    name={itemStore.ALL_ITEMS}
                    active={itemStore.navState === itemStore.ALL_ITEMS}
                    onClick={handleNavClick}
                />
                <Menu.Item as={NavLink} to={"/watchList"}
                    name={itemStore.WATCHLIST}
                    active={itemStore.navState === itemStore.WATCHLIST}
                    onClick={handleNavClick}
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
