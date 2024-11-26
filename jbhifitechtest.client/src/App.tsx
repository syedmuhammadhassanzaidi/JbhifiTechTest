import { useEffect, useState } from 'react';
import './App.css';
//import styles from "./weatherform.module.css";

interface Forecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

function App() {
    const [forecasts, setForecasts] = useState<Forecast[]>();
    const [api, setApi] = useState<string>("");
    const [city, setCity] = useState<string>("");
    const [country, setCounntry] = useState<string>("");

    const handleApiChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setApi(event.target.value);
        console.log(api);
    }

    const handleCityChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCity(event.target.value);
        console.log(city);
    }

    const handleCountryChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCounntry(event.target.value);
        console.log(country);
    }
    
    useEffect(() => {
        populateWeatherData();
    }, []);

    // const contents = forecasts === undefined
    //     ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
    //     : <table className="table table-striped" aria-labelledby="tableLabel">
    //         <thead>
    //             <tr>
    //                 <th>Date</th>
    //                 <th>Temp. (C)</th>
    //                 <th>Temp. (F)</th>
    //                 <th>Summary</th>
    //             </tr>
    //         </thead>
    //         <tbody>
    //             {forecasts.map(forecast =>
    //                 <tr key={forecast.date}>
    //                     <td>{forecast.date}</td>
    //                     <td>{forecast.temperatureC}</td>
    //                     <td>{forecast.temperatureF}</td>
    //                     <td>{forecast.summary}</td>
    //                 </tr>
    //             )}
    //         </tbody>
    //     </table>;

    return (
        <div>
            {/* <div> */}
            <div className="form-control">
                    <label>Api Key</label>
                    <input type="text" id="api" onChange={handleApiChange} />
                </div>
                <div className="form-control">
                    <label>City</label>
                    <input type="text" id="city" onChange={handleCityChange} />
                </div>
                <div className="form-control">
                    <label>Country</label>
                    <input type="text" id="country" onChange={handleCountryChange} />
                </div>
            {/* </div> */}
            <div>
                <button /*disabled={ButtonDisabled}*/ onClick={populateWeatherData}>Submit</button>
            </div>
        </div>
    //     <div>
    //   <div>
    //     <div
    //       className={`${styles["form-control"]} ${
    //         !isValidApiKey && styles.invalid
    //       }`}
    //     >
    //       <label>Api Key</label>
    //       <input type="text" onChange={ApiKeyInputChangeHandler} />
    //     </div>
    //     <div
    //       className={`${styles["form-control"]} ${
    //         !isValidCity && styles.invalid
    //       }`}
    //     >
    //       <label>City</label>
    //       <input type="text" onChange={CityInputChangeHandler} />
    //     </div>
    //     <div
    //       className={`${styles["form-control"]} ${
    //         !isValidCountry && styles.invalid
    //       }`}
    //     >
    //       <label>Country</label>
    //       <input type="text" onChange={CountryInputChangeHandler} />
    //     </div>
    //   </div>
    //   <Button
    //     type="submit"
    //     disabled={
    //       !validApiKey.test(apiKey) || !city.length > 0 || !country.length > 0
    //     }
    //     onClick={GetWeatherHandler}
    //   >
    //     Get Weather Details
    //   </Button>
    // </div>
    );

    async function populateWeatherData() {
        const response = await fetch('weatherforecast?city='+city+'&&country='+country,
            {
                headers: {
                    "api-key": api,
                }
            }
        );
        //console.log(response);
        if (response.ok) {
            const data = await response.formData;
            console.log("The Response is: " + data);
            //setForecasts(data);
        }
    }
}

export default App;