import axios from "axios";

// Prefer Docker/compose env var when available, fall back to appsettings.json for local dev.
const settings = require("../appsettings.json");
const env = settings.CurrentEnvironment;

export const API_BASE_URL: string =
  process.env.REACT_APP_API_URL || settings[env].url;

export const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    "content-type": "application/json",
  },
  withCredentials: true,
});