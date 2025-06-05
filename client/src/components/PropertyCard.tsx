import React, { useState } from 'react';
import { Property } from '../types';
import SpaceList from './SpaceList';

interface PropertyCardProps {
    property: Property;
}

const PropertyCard: React.FC<PropertyCardProps> = ({ property }) => {
    const [isExpanded, setIsExpanded] = useState(false);

    return (
        <div className="property-card">
            <div 
                className="property-header"
                onClick={() => setIsExpanded(!isExpanded)}
                style={{ cursor: 'pointer' }}
            >
                <h2>{property.name}</h2>
                <span>{isExpanded ? '▼' : '▶'}</span>
            </div>

            {isExpanded && (
                <div className="property-details">
                    <p>{property.description}</p>

                    <div className="features">
                        <h3>Features</h3>
                        <ul>
                            {property.features.map((feature, index) => (
                                <li key={index}>{feature}</li>
                            ))}
                        </ul>
                    </div>

                    <div className="highlights">
                        <h3>Highlights</h3>
                        <ul>
                            {property.highlights.map((highlight, index) => (
                                <li key={index}>{highlight}</li>
                            ))}
                        </ul>
                    </div>

                    <div className="transportation">
                        <h3>Transportation</h3>
                        <ul>
                            {property.transportation.map((transport, index) => (
                                <li key={index}>
                                    {transport.name} ({transport.distance})
                                </li>
                            ))}
                        </ul>
                    </div>

                    <SpaceList spaces={property.spaces} />
                </div>
            )}
        </div>
    );
};

export default PropertyCard; 