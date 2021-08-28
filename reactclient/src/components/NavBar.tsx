import { observer } from "mobx-react-lite";
import { Menu, Image, Dropdown, DropdownMenu } from "semantic-ui-react";
import { useStore } from "../stores/store";
import { Link, NavLink } from "react-router-dom";

const NavBar = () => {

    const { itemStore, userStore: {user, logout} } = useStore();

    const handleNavClick = async (e: any, { name }: any) => {
        itemStore.setNavState(name);
        itemStore.setCheckedItems([]);
    }

    return (
        <Menu>
            <Menu.Item as={NavLink} to={"/"}>
                <img style={{width: '30px', height: 'auto'}} src="/logo512.png"/>
                <div style={{margin: '10px'}}>OSRSPredator</div>
            </Menu.Item>

            <Menu pointing secondary className="right">
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
                <Menu.Item as={NavLink} to='/errors' name='Errors' />
            </Menu>
            
            <Menu pointing secondary className="right">
                <Menu.Item>
                    <i className="large icon bell outline"/>
                </Menu.Item>
                <Menu.Item>
                    <i className="large icon mail outline"/>
                </Menu.Item>
                <Menu.Item>
                    <i className="large icon home"/>
                </Menu.Item>
                <Menu.Item position='right'>
                    <Image src={user?.image || '/logo192.png'} avatar spaced='right'></Image>
                    <Dropdown pointing='top left' text={user?.displayName}>
                        <DropdownMenu>
                            <Dropdown.Item as={Link} to={`/profile/${user?.username}`} text='My Profile' icon='user' />
                            <Dropdown.Item onClick={logout} text='Logout' icon='power' />
                        </DropdownMenu> 
                    </Dropdown>
                </Menu.Item>
            </Menu>
        </Menu>      
    )
}

export default observer(NavBar);
