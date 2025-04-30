$(document).ready(function() {
    let cart = [];

    // API URL for fetching products
    const apiUrl = "https://fakestoreapi.com/products";

    // Fetch all products
    function fetchProducts() {
        $.get(apiUrl, function(products) {
            displayProducts(products);
        }).fail(function() {
            alert("Failed to fetch products. Please try again later.");
        });
    }

    // Display product list
    function displayProducts(products) {
        products.forEach(product => {
            console.log("hi reloading");
            $(".products").append(`
                <div class="product" data-id="${product.id}">
                    <img src="${product.image}" alt="${product.title}" style="width:100px; height:100px;">
                    <h3>${product.title}</h3>
                    <p>Price: ₹${product.price}</p>
                    <button class="view-details" data-id="${product.id}">View Details</button>
                    <button class="add-to-cart" data-name="${product.title}" data-price="${product.price}">Add to Cart</button>
                </div>
            `);
        });
    }

    // Fetch and display product details dynamically
    function fetchProductDetails(productId) {
        $.get(`${apiUrl}/${productId}`, function(product) {
            $("#product-details").html(`
                <div>
                    <img src="${product.image}" alt="${product.title}" style="width:150px; height:150px;">
                    <h2>${product.title}</h2>
                    <p>${product.description}</p>
                    <p>Price: ₹${product.price}</p>
                </div>
            `).fadeIn(); // Show product details dynamically
        }).fail(function() {
            alert("Failed to load product details. Please try again.");
        });
    }

    // Update the cart display
    function updateCart() {
        let cartTable = $("#cart-table tbody");
        cartTable.empty();
        let total = 0;

        cart.forEach((item, index) => {
            let subtotal = item.price * item.quantity;
            total += subtotal;

            cartTable.append(`
                <tr>
                    <td>${item.name}</td>
                    <td>₹${item.price}</td>
                    <td>
                        <button class="decrease" data-index="${index}">-</button>
                        ${item.quantity}
                        <button class="increase" data-index="${index}">+</button>
                    </td>
                    <td>₹${subtotal}</td>
                    <td><button class="remove" data-index="${index}">Remove</button></td>
                </tr>
            `);
        });

        $("#total").text(`Total: ₹${total}`);
    }

    // Add a product to the cart
    $(document).on("click", ".add-to-cart", function() {
        let name = $(this).data("name");
        let price = parseFloat($(this).data("price"));

        let existingItem = cart.find(item => item.name === name);
        if (existingItem) {
            existingItem.quantity++;
        } else {
            cart.push({ name, price, quantity: 1 });
        }

        updateCart();
    });

    // Increase quantity
    $(document).on("click", ".increase", function() {
        let index = $(this).data("index");
        cart[index].quantity++;
        updateCart();
    });

    // Decrease quantity
    $(document).on("click", ".decrease", function() {
        let index = $(this).data("index");
        if (cart[index].quantity > 1) {
            cart[index].quantity--;
        } else {
            cart.splice(index, 1);
        }
        updateCart();
    });

    // Remove a product from the cart
    $(document).on("click", ".remove", function() {
        let index = $(this).data("index");
        cart.splice(index, 1);
        updateCart();
    });

    // View product details
    $(document).on("click", ".view-details", function() {
        const productId = $(this).data("id");
        fetchProductDetails(productId); // Dynamically load product details
    });

    // Initial load of products
    fetchProducts();
});
