import React, { useState } from "react";
import axiosClient from "../../api/axiosClient";

const CheckoutPage = () => {
  const [status, setStatus] = useState("idle");

  const handleCheckout = async () => {
    setStatus("loading");
    try {
      await axiosClient.post("/orders/checkout", {
        customerId: 1,
        currency: "USD",
        items: [
          {
            productId: 1,
            quantity: 1,
          },
        ],
      });
      setStatus("success");
    } catch (error) {
      setStatus("error");
    }
  };

  return (
    <section>
      <h2>Checkout</h2>
      <button type="button" onClick={handleCheckout}>
        Place Order
      </button>
      {status === "loading" && <p>Processing payment...</p>}
      {status === "success" && <p>Order placed successfully.</p>}
      {status === "error" && <p>Payment failed. Please retry.</p>}
    </section>
  );
};

export default CheckoutPage;
