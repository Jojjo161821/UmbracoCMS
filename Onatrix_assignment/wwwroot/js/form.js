document.addEventListener("submit", function (e) {
    if (e.target.matches("form")) {
        sessionStorage.setItem("scrollY", window.scrollY);
    }
})

document.addEventListener("DOMContentLoaded", function () {
    const scrollY = sessionStorage.getItem("scrollY");
    if (scrollY) {
        window.scrollTo({ top: parseInt(scrollY, 10), behavior: "instant" });
        sessionStorage.removeItem("scrollY");
    }
});

const validateField = (field) => {
    let spanError = document.querySelector(`span[data-valmsg-for='${field.name}']`)
    if (!spanError) return;

    let errorMessage = ""
    let value = field.value.trim()

    if (field.hasAttribute("data-val-required") && value === "")
        errorMessage = field.getAttribute("data-val-required")

    if (field.hasAttribute("data-val-regex") && value !== "") {
        let pattern = new RegExp(field.getAttribute("data-val-regex-pattern"))
        if (!pattern.test(value))
            errorMessage = field.getAttribute("data-val-regex")
    }

    if (errorMessage) {
        spanError.classList.remove("field-validation-valid")
        spanError.classList.add("field-validation-error")
        field.classList.remove("input-validation-valid")
        field.classList.add("input-validation-error")
        spanError.textContent = errorMessage
    } else {
        spanError.classList.add("field-validation-valid")
        spanError.classList.remove("field-validation-error")
        field.classList.remove("input-validation-error")
        field.classList.add("input-validation-valid")
        spanError.textContent = ""
    }
}

document.addEventListener("DOMContentLoaded", () => {
    const fields = document.querySelectorAll(
        "input[data-val='true'], textarea[data-val='true'], select[data-val='true']"
    );

    fields.forEach(field => {
        field.addEventListener("input", () => validateField(field));
    });
});
