import React from "react";

interface TabbedViewProps {
    tabs: string[];
    content: React.ReactNode[];
}

const TabbedView: React.FC<TabbedViewProps> = ({ tabs, content }) => {
    const [activeIndex, setActiveIndex] = React.useState(0);

    return (
        <div>
            <ul className="nav nav-tabs">
                {tabs.map((tab, index) => (
                    <li className="nav-item" key={index}>
                        <button
                            className={`nav-link ${activeIndex === index ? "active" : ""}`}
                            onClick={() => setActiveIndex(index)}
                        >
                            {tab}
                        </button>
                    </li>
                ))}
            </ul>
            <div className="tab-content mt-3">
                {content.map((item, index) => (
                    <div
                        key={index}
                        className={`tab-pane ${activeIndex === index ? "active show" : ""}`}
                    >
                        {item}
                    </div>
                ))}
            </div>
        </div>
    );
};

export default TabbedView;
