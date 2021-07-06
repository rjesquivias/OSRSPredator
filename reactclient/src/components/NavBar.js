import { useState } from "react";

const NavBar = () => {
    const [activeItem, setActiveItem] = useState("Option1");

    const handleItemClick = (e, name) => setActiveItem(name);

    return (
        <div className="ui secondary pointing menu massive">
            <div className="item">
                <img style={{width: '30px', height: 'auto'}} src="/logo512.png"/>
                <div style={{margin: '10px'}}>OSRSPredator</div>
            </div>
            <div className="right menu">
                <a className={activeItem === "Option1" ? "item active" : "item"} onClick={(e) => handleItemClick(e, "Option1")}>
                    Dashboard
                </a>
                <a className={activeItem === "Option2" ? "item active" : "item"} active={'false'} onClick={(e) => handleItemClick(e, "Option2")}>
                    Messages
                </a>
                <a className={activeItem === "Option3" ? "item active" : "item"} active={'false'} onClick={(e) => handleItemClick(e, "Option3")}>
                    Friends
                </a>
            </div>
            
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
