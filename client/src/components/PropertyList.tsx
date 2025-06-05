import React, { useState, useEffect } from 'react';
import { Property } from '../types';
import PropertyCard from './PropertyCard';
import config from '../config';

const PropertyList: React.FC = () => {
    const [properties, setProperties] = useState<Property[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchProperties = async () => {
            try {
                const response = await fetch(`${config.apiUrl}/api/Properties`);
                if (!response.ok) {
                    throw new Error('Failed to fetch properties');
                }
                const data = await response.json();
                setProperties(data);
            } catch (err) {
                setError(err instanceof Error ? err.message : 'An error occurred');
            } finally {
                setLoading(false);
            }
        };

        fetchProperties();
    }, []);

    if (loading) return <div>Loading properties...</div>;
    if (error) return <div>Error: {error}</div>;

    return (
        <div className="property-list">
            {properties.map((property) => (
                <PropertyCard key={property.id} property={property} />
            ))}
        </div>
    );
};

export default PropertyList; 