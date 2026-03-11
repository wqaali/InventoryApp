import { useEffect, useState } from "react"
import { AgGridReact } from "ag-grid-react"
import { ColDef, ModuleRegistry, AllCommunityModule } from "ag-grid-community"
import * as signalR from "@microsoft/signalr"

import { Stock } from "../Types/Stock"
import { getStocks } from "../Api/CallApi"
import { connection } from "../Api/Signalr"

import "ag-grid-community/styles/ag-grid.css"
import "ag-grid-community/styles/ag-theme-alpine.css"

ModuleRegistry.registerModules([AllCommunityModule])

const StockGrid = () => {

  const [stocks, setStocks] = useState<Stock[]>([])

  const columnDefs: ColDef[] = [
    { field: "id", headerName: "ID" },
    { field: "symbol", headerName: "Symbol" },
    { field: "price", headerName: "Price" },
    { field: "quantity", headerName: "Quantity" }
  ]

  useEffect(() => {

    loadStocks()
    startSignalR()

  }, [])

  const loadStocks = async () => {
    try {

      const data = await getStocks()
      setStocks(data)

    } catch {

      console.log("API failed (expected 10%)")

    }
  }

  const startSignalR = async () => {

    if (connection.state === signalR.HubConnectionState.Disconnected) {
      await connection.start()
    }

    connection.off("ReceiveStocks")
    connection.on("ReceiveStocks", (data: Stock[]) => {
      setStocks([...data])
    })

  }

  return (

    <div
      className="ag-theme-alpine"
      style={{ height: 500, width: 700 }}
    >

      <AgGridReact
        rowData={stocks}
        columnDefs={columnDefs}
        pagination={true}
      />

    </div>
  )
}

export default StockGrid