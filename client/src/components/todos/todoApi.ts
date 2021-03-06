import { Todo } from "./todoSlice";
import { apiFetch, ApiResponse } from "../../utils/apiFetch";

export function fetchTodos(uid: number | null) {
  return new Promise<ApiResponse<Todo[]>>((resolve) => {
    apiFetch("Todo").then((res: ApiResponse<Todo[]>) => {
      resolve(res);
    });
  });
}

export function createTodo(title: string, description: string) {
  return new Promise<ApiResponse<Todo>>((resolve) => {
    apiFetch("Todo", "POST", { title: title, description: description }).then((res: ApiResponse<Todo>) => {
      resolve(res);
    });
  });
}

export function updateTodo(id: number, patchTodo: Partial<Todo>) {
  return new Promise<ApiResponse<Todo>>((resolve) => {
    apiFetch(`Todo/${id}`, "PATCH", patchTodo).then((res: ApiResponse<Todo>) => {
      resolve(res);
    });
  });
}

export function deleteTodos(ids: number[]) {
  return new Promise<ApiResponse<number[]>>((resolve) => {
    apiFetch("Todo", "DELETE", { ids: ids }).then((res: ApiResponse<number[]>) => {
      resolve(res);
    });
  });
}
