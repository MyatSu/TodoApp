const apiUrl = "/api/todo";

async function loadTodos() {
  const res = await fetch(apiUrl);
  const todos = await res.json();

  const list = document.getElementById("todoList");
  list.innerHTML = ""; // clear existing

  todos.map((todo) => {
    const li = document.createElement("li");
    li.textContent = `${todo.title} - ${todo.isComplete ? "Completed" : "Pending"}`;
    list.appendChild(li);
  });
}

async function addTodo() {
  const title = document.getElementById("todoTitle").value.trim();
  if (!title) return alert("Please enter a title");

  const newTodo = { title, isComplete: false };

  await fetch(apiUrl, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newTodo),
  });

  document.getElementById("todoTitle").value = "";
  loadTodos();
}

async function deleteTodo(id) {
  await fetch(`${apiUrl}/${id}`, {
    method: "DELETE",
  });
  loadTodos();
}

async function searchByTitle() {
  const searchInput = document.getElementById("todoTitle");
  const searchText = searchInput.value.trim().toLowerCase();

  const res = await fetch(apiUrl);
  const todos = await res.json();

  const filtered = todos.filter((t) =>
    t.title.toLowerCase().includes(searchText)
  );

  const list = document.getElementById("todoList");
  list.innerHTML = ""; // clear existing

  filtered.map((todo) => {
    const li = document.createElement("li");
    li.textContent = `${todo.title} - ${todo.isComplete ? "Completed" : "Pending"}`;
    list.appendChild(li);
  });
}

document.addEventListener("DOMContentLoaded", () => {
  loadTodos();
  console.log(document.getElementById("todoList"));
});
