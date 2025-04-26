$(document).ready(function() {
    let cart = [];

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

    $(".add-to-cart").click(function() {
        let name = $(this).data("name");
        let price = $(this).data("price");

        let existingItem = cart.find(item => item.name === name);
        if (existingItem) {
            existingItem.quantity++;
        } else {
            cart.push({ name, price, quantity: 1 });
        }

        updateCart();
    });

    $(document).on("click", ".increase", function() {
        let index = $(this).data("index");
        cart[index].quantity++;
        updateCart();
    });

    $(document).on("click", ".decrease", function() {
        let index = $(this).data("index");
        if (cart[index].quantity > 1) {
            cart[index].quantity--;
        } else {
            cart.splice(index, 1);
        }
        updateCart();
    });

    $(document).on("click", ".remove", function() {
        let index = $(this).data("index");
        cart.splice(index, 1);
        updateCart();
    });
});
