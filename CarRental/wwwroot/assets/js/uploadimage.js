const imageInput = document.querySelector("#imageInput");

imageInput.addEventListener("change", function () {
    const reader = new FileReader();
    reader.addEventListener("load", () => {
        const uploadedImage = reader.result;
        const displayDiv = document.querySelector("#display");

        // Xóa nội dung hiện tại (ảnh cũ hoặc thông báo "Chưa có ảnh nào")
        displayDiv.innerHTML = "";

        // Tạo thẻ <img> và hiển thị ảnh mới
        const img = document.createElement("img");
        img.src = uploadedImage;
        img.alt = "Ảnh mặt trước GPLX";
        img.style.maxWidth = "100%";
        img.style.maxHeight = "100%";
        img.style.borderRadius = "10px";

        displayDiv.appendChild(img);
    });

    // Đọc file người dùng vừa chọn
    reader.readAsDataURL(this.files[0]);
});
