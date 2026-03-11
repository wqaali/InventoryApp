import * as signalR from "@microsoft/signalr"
import { API_BASE_URL } from "./BaseUrl"

export const connection = new signalR.HubConnectionBuilder()
  .withUrl(API_BASE_URL+"/stockhub")
  .withAutomaticReconnect()
  .build()