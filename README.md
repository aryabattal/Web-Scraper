# Web Scraper

## Description
This application is a web scraper built in C# using the HtmlAgilityPack library. It scrapes the content of a website, including pages, CSS files, and images, and saves them locally for offline browsing. The scraper is designed to provide progress updates during the scraping process and ensures that all images are visible in the local files.

## Design Thoughts
The design of the code focuses on modularity and readability.Each function has a clear purpose, such as downloading pages, CSS files, and images, which improves code maintainability and makes it easier to understand. Progress updates are included to provide feedback to the user during the scraping process. Additionally, error handling is implemented to handle potential issues during file downloads.
The application will clean up the directory before each run.

## Installation
1. Clone or download the repository to your local machine.
2. Ensure you have the necessary dependencies installed, including the HtmlAgilityPack library.
3. Build the application using your preferred IDE or compiler.

## Usage
1. Update the `baseUrl` variable in the `Main` method with the URL of the website you want to scrape.
2. Update the `outputDirectory` variable with the desired directory where you want to save the scraped content.
3. Run the application. It will scrape the website and save the content locally.

## Dependencies
- HtmlAgilityPack (install via NuGet)

## How to Contribute
1. Fork the repository.
2. Make your changes.
3. Submit a pull request.

## License
This project is licensed under the [MIT License](LICENSE).
