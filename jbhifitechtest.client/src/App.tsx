import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [api, setApi] = useState<string>("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
    const [city, setCity] = useState<string>("");
    const [country, setCountry] = useState<string>("");
    const [apiError, setApiError] = useState<string>("");
    const [cityError, setCityError] = useState<string>("");
    const [countryError, setCountryError] = useState<string>("");
    const [weatherMessage, setWeatherMessage] = useState<string>("");

    const validateApiKey = (value: string) => {
        const isValidLength = value.length === 32;
        const isValidFormat = /^[a-z0-9]+$/.test(value);
        return isValidLength && isValidFormat;
    }

    const handleApiChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value;
        setApi(value);

        if (!validateApiKey(value)) {
            setApiError("API key must be 32 characters long and contain only lowercase letters and numbers.")
        }
        else {
            setApiError("");
        }
    }

    const handleCityChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const value = (event.target.value);
        setCity(value);

        if (!value.trim()) {
            setCityError("City must not be empty.");
        } 
        else {
            setCityError("");
        }
    }

    const handleCountryChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const value = (event.target.value);
        setCountry(value);

        if (!value.trim()) {
            setCountryError("Country must not be empty.");
        }
        else {
            setCountryError("");
        }
    }
    
    useEffect(() => {
        populateWeatherData();
    }, []);

    return (
        <div>
            <div className="form-control">
                <label>Api Key</label>
                <input type="text" id="api" onChange={handleApiChange} style={{ width: "650px" }} />
                <label style={{color: "red", marginLeft: "10px", display: apiError ? "inline" : "none",}}>
                    API Key must be 32 characters long and contain only lowercase letters
                    and numbers.
                </label>
            </div>
            <div className="form-control">
                <label>City</label>
                <input type="text" id="city" onChange={handleCityChange} style={{ width: "650px" }} />
                <label style={{color: "red", marginLeft: "10px", display: cityError ? "inline" : "none",}}>
                    City cannot be empty.
                </label>
            </div>
            <div className="form-control">
                <label>Country</label>
                <input type="text" id="country" onChange={handleCountryChange} style={{ width: "650px" }} />
                <label style={{color: "red", marginLeft: "10px", display: countryError ? "inline" : "none",}}>
                    Country cannot be empty.
                </label>
            </div>
            <div>
                <button disabled={!!apiError || !!cityError || !!countryError || !api || !city || !country} onClick={populateWeatherData}>Get Weather</button>
            </div>
            <div>
                <label style={{marginTop: "10px", display: "block", fontWeight: "bold",}}>
                    {weatherMessage}
                </label>
            </div>
        </div>
    );

    async function populateWeatherData() {
        const response = await fetch('weatherforecast?city='+city+'&&country='+country,
            {
                headers: {
                    "api-key": api,
                }
            }
        );
        const data = await response.text();
        setWeatherMessage(data);
    }
}

export default App;