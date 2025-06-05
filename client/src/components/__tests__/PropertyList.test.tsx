import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom';
import PropertyList from '../PropertyList';

// Mock the fetch function
global.fetch = jest.fn();

describe('PropertyList', () => {
    const mockProperties = [
        {
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
        }
    ];

    beforeEach(() => {
        (global.fetch as jest.Mock).mockClear();
    });

    it('renders loading state initially', () => {
        render(<PropertyList />);
        expect(screen.getByText('Loading properties...')).toBeInTheDocument();
    });

    it('renders properties when data is loaded', async () => {
        (global.fetch as jest.Mock).mockResolvedValueOnce({
            ok: true,
            json: async () => mockProperties
        });

        render(<PropertyList />);

        await waitFor(() => {
            expect(screen.getByText('Test Property')).toBeInTheDocument();
        });
    });

    it('renders error state when fetch fails', async () => {
        (global.fetch as jest.Mock).mockRejectedValueOnce(new Error('Failed to fetch'));

        render(<PropertyList />);

        await waitFor(() => {
            expect(screen.getByText('Error: Failed to fetch')).toBeInTheDocument();
        });
    });

    it('renders error state when response is not ok', async () => {
        (global.fetch as jest.Mock).mockResolvedValueOnce({
            ok: false
        });

        render(<PropertyList />);

        await waitFor(() => {
            expect(screen.getByText('Error: Failed to fetch properties')).toBeInTheDocument();
        });
    });
}); 