import axios from "axios"
import { api } from "./BaseUrl"

export const getStocks = async () => {
  const response = await api.get("api/stocks")
  return response.data
}