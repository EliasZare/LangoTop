const cookieName = "cart-items";

function addToCart(id, name, price, picture) {
    let products = $.cookie("cart-items");
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }

    const count = 1;
    const currentProduct = products.find(x => x.id === id);
    if (currentProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            unitPrice: price,
            picture,
            count
        }

        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function updateCart() {
    debugger;
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#cart_items_count").text(products.length);
    $("#cart_items_count_Mobile").text(products.length);
    const cartItemsWrapper = $("#cart_items_wrapper");
    cartItemsWrapper.html('');
    products.forEach(x => {
        const product =
            `<div class="ground ground-list-single">
                      <div class="grd_thum"><img src="/Pictures/${x.picture}" class="img-fluid rounded" width="90px" style="border-radius: 10px;" alt="${x.name}" /></div>
                      <div class="ground-content">
                          <h6>${x.name}<small class="float-left text-fade">${x.unitPrice}Ъ</small></h6>
                          <a  class="small text-danger" onclick="removeFromCart('${x.id}')>Эан</a>
                      </div>
             </div>`;
        cartItemsWrapper.append(product);
    });

    const cartItemsWrapperMobile = $("#cart_items_wrapper_mobile");
    cartItemsWrapperMobile.html('');
    products.forEach(x => {
        const product =
            `<div class="ground ground-list-single">
                      <div class="grd_thum"><img src="/Pictures/${x.picture}" class="img-fluid rounded" width="90px" style="border-radius: 10px;" alt="${x.name}" /></div>
                      <div class="ground-content">
                          <h6>${x.name}<small class="float-left text-fade">${x.unitPrice}Ъ</small></h6>
                          <a  class="small text-danger" onclick="removeFromCart('${x.id}')>Эан</a>
                      </div>
             </div>`;
        cartItemsWrapperMobile.append(product);
    });
}

function removeFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    const itemToRemove = products.findIndex(x => x.id === id);
    products.splice(itemToRemove, 1);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function changeCartItemCount(id, totalId, count) {
    var products = $.cookie(cookieName);
    products = JSON.parse(products);
    const productIndex = products.findIndex(x => x.id == id);
    products[productIndex].count = count;
    const product = products[productIndex];
    const newPrice = parseInt(product.unitPrice) * parseInt(count);
    $(`#${totalId}`).text(newPrice);
    products[productIndex].totalPrice = newPrice;
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
};