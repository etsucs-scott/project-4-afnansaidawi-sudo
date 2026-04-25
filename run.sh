#!/bin/bash
# Build and Run Instructions for MusicPracticeScheduler

echo "Building the project..."
dotnet build

if [ $? -eq 0 ]; then
    echo "Build successful!"
    echo ""
    echo "Running tests..."
    dotnet test
    
    if [ $? -eq 0 ]; then
        echo ""
        echo "All tests passed!"
        echo ""
        echo "Starting the application at http://localhost:5001"
        dotnet run --project MusicPracticeScheduler.App
    else
        echo "Tests failed. Please check the output above."
    fi
else
    echo "Build failed. Please check the output above."
fi
