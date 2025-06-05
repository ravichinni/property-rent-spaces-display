import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom';
import PropertyCard from '../PropertyCard';
import { Property } from '../../types';

describe('PropertyCard', () => {
    const mockProperty: Property = {
        id: '1',
        name: 'Test Property',
        description: 'Test Description',
        features: ['Feature 1', 'Feature 2'],
        highlights: ['Highlight 1'],
        transportation: [
            { name: 'Bus', distance: '0.5 miles' }
        ],
        spaces: [
            {
                id: '1',
                name: 'Space 1',
                description: 'Space Description',
                rentRoll: [
                    { month: 'January', amount: 1000 }
                ]
            }
        ]
    };

    it('renders property name', () => {
        render(<PropertyCard property={mockProperty} />);
        expect(screen.getByText('Test Property')).toBeInTheDocument();
    });

    it('shows expand/collapse indicator', () => {
        render(<PropertyCard property={mockProperty} />);
        expect(screen.getByText('▶')).toBeInTheDocument();
    });

    it('expands and shows details when clicked', () => {
        render(<PropertyCard property={mockProperty} />);
        
        // Initially collapsed
        expect(screen.queryByText('Test Description')).not.toBeInTheDocument();
        
        // Click to expand
        fireEvent.click(screen.getByText('Test Property'));
        
        // Check if details are shown
        expect(screen.getByText('Test Description')).toBeInTheDocument();
        expect(screen.getByText('Feature 1')).toBeInTheDocument();
        expect(screen.getByText('Feature 2')).toBeInTheDocument();
        expect(screen.getByText('Highlight 1')).toBeInTheDocument();
        expect(screen.getByText('Bus (0.5 miles)')).toBeInTheDocument();
    });

    it('collapses when clicked again', () => {
        render(<PropertyCard property={mockProperty} />);
        
        // Expand first
        fireEvent.click(screen.getByText('Test Property'));
        expect(screen.getByText('Test Description')).toBeInTheDocument();
        
        // Collapse
        fireEvent.click(screen.getByText('Test Property'));
        expect(screen.queryByText('Test Description')).not.toBeInTheDocument();
    });

    it('changes expand/collapse indicator when toggled', () => {
        render(<PropertyCard property={mockProperty} />);
        
        // Initially collapsed
        expect(screen.getByText('▶')).toBeInTheDocument();
        
        // Expand
        fireEvent.click(screen.getByText('Test Property'));
        expect(screen.getByText('▼')).toBeInTheDocument();
        
        // Collapse
        fireEvent.click(screen.getByText('Test Property'));
        expect(screen.getByText('▶')).toBeInTheDocument();
    });
}); 