const apiUrl = "https://localhost:<port>/api/HangHoa";

// Load danh sách hàng hóa
async function fetchGoods() {
    try {
        const response = await fetch(apiUrl);
        const goods = await response.json();

        const goodsList = document.getElementById("goods-list");
        goodsList.innerHTML = "";

        goods.forEach(good => {
            const li = document.createElement("li");
            li.innerHTML = `
                ${good.maHangHoa} - ${good.tenHangHoa} - SL: ${good.soLuong} - Ghi chú: ${good.ghiChu || "N/A"}
                <button onclick="deleteGood('${good.maHangHoa}')">Xóa</button>
                <button onclick="editGhiChu('${good.maHangHoa}')">Sửa ghi chú</button>
            `;
            goodsList.appendChild(li);
        });
    } catch (error) {
        console.error("Error fetching goods:", error);
    }
}

// Thêm mới hàng hóa
document.getElementById("add-good-form").addEventListener("submit", async (e) => {
    e.preventDefault();

    const newGood = {
        maHangHoa: document.getElementById("maHangHoa").value,
        tenHangHoa: document.getElementById("tenHangHoa").value,
        soLuong: parseInt(document.getElementById("soLuong").value),
        ghiChu: document.getElementById("ghiChu").value
    };

    try {
        await fetch(apiUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(newGood)
        });

        fetchGoods(); // Cập nhật danh sách
    } catch (error) {
        console.error("Error adding good:", error);
    }
});

// Xóa hàng hóa
async function deleteGood(maHangHoa) {
    try {
        await fetch(`${apiUrl}/${maHangHoa}`, {
            method: "DELETE"
        });

        fetchGoods(); // Refresh danh sách
    } catch (error) {
        console.error("Error deleting good:", error);
    }
}

// Sửa ghi chú
async function editGhiChu(maHangHoa) {
    const ghiChuMoi = prompt("Nhập ghi chú mới:");
    if (!ghiChuMoi) return;

    try {
        await fetch(`${apiUrl}/${maHangHoa}/ghi-chu`, {
            method: "PATCH",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(ghiChuMoi)
        });

        fetchGoods(); // Refresh danh sách
    } catch (error) {
        console.error("Error updating note:", error);
    }
}

fetchGoods(); // Load dữ liệu ban đầu

