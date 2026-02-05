import React, { useEffect, useState } from "react";
import axiosClient from "../../api/axiosClient";

const ProductList = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadProducts = async () => {
      try {
        const response = await axiosClient.get("/products");
        setProducts(response.data);
      } catch (error) {
        setProducts([]);
      } finally {
        setLoading(false);
      }
    };

    loadProducts();
  }, []);

  if (loading) {
    return <p>Loading products...</p>;
  }

  return (
    <section>
      <h2>Products</h2>
      <ul>
        {products.map((product) => (
          <li key={product.id}>
            <strong>{product.productName}</strong> — ${product.unitPrice}
          </li>
        ))}
      </ul>
    </section>
  );
};

export default ProductList;
