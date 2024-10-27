import './style.css'

import { ProductsApiClient } from '../../client/ProductsApiClient'

const products = document.getElementById("products") as HTMLDivElement;

async function getAllProducts() {
  const client = new ProductsApiClient("http://localhost:5227");
  const data = await client.getAllProducts();
  const serializedData = JSON.stringify(data, null, 2);

  products.innerHTML = `<pre>${serializedData}</pre>`;
}

getAllProducts();