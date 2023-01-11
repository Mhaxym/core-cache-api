## Details
This is just a small prohect to play around with **.NET CORE**. It's a simple Web API application that allows you to read and set `album` data. It's also implementing a simple in memory cache that uses the singleton pattern.

## How to run
1. Clone the repository
2. Run `docker-compose up` in the root folder. You could add the `--build` flag if you want to rebuild the images.
3. Open your browser and navigate to `http://localhost:5000/Album` to see the list of albums.
4. You can also use the Swagger UI to test the API. Navigate to `http://localhost:5000/swagger/index.html` to see the Swagger UI.

If you want to run it using SSL, you can use the `docker-compose-ssl.yml` file. You will need to generate a self signed certificate. To build the image, run `docker-compose -f docker-compose-ssl.yml up --build` and uncomment `app.UseHttpsRedirection();` from  `Program.cs` file. You can also use the `--build` flag with the `docker-compose.yml` file.

## How to test it
Using POSTMAN you could just send a GET request to `http://localhost:5000/Album` to get the list of albums. You can also use the Swagger UI to test the API. Navigate to `http://localhost:5000/swagger/index.html` to see the Swagger UI.