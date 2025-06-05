import React, { useState } from 'react';
import { Space } from '../types';

interface SpaceListProps {
    spaces: Space[];
}

const SpaceList: React.FC<SpaceListProps> = ({ spaces }) => {
    const [expandedSpaceId, setExpandedSpaceId] = useState<string | null>(null);

    return (
        <div className="space-list">
            <h3>Spaces</h3>
            {spaces.map((space) => (
                <div key={space.id} className="space-item">
                    <div
                        className="space-header"
                        onClick={() => setExpandedSpaceId(
                            expandedSpaceId === space.id ? null : space.id
                        )}
                        style={{ cursor: 'pointer' }}
                    >
                        <h4>{space.name}</h4>
                        <span>{expandedSpaceId === space.id ? '▼' : '▶'}</span>
                    </div>

                    {expandedSpaceId === space.id && (
                        <div className="space-details">
                            <p>{space.description}</p>
                            <div className="rent-roll">
                                <h5>Rent Roll</h5>
                                <table>
                                    <thead>
                                        <tr>
                                            <th>Month</th>
                                            <th>Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {space.rentRoll.map((rent, index) => (
                                            <tr key={index}>
                                                <td>{rent.month}</td>
                                                <td>${rent.amount.toLocaleString()}</td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    )}
                </div>
            ))}
        </div>
    );
};

export default SpaceList; 