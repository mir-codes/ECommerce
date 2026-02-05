import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import ProductList from "./features/products/ProductList";
import CartSummary from "./features/cart/CartSummary";
import CheckoutPage from "./features/checkout/CheckoutPage";

const App = () => (
  <div style={{ padding: "2rem", fontFamily: "Arial, sans-serif" }}>
    <h1>E-Commerce Storefront</h1>
    <Routes>
      <Route path="/" element={<ProductList />} />
      <Route path="/cart" element={<CartSummary />} />
      <Route path="/checkout" element={<CheckoutPage />} />
      <Route path="*" element={<Navigate to="/" />} />
    </Routes>
  </div>
);

export default App;
