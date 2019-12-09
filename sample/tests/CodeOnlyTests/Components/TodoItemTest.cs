﻿using Egil.RazorComponents.Testing.Asserting;
using Egil.RazorComponents.Testing.Library.SampleApp.Components;
using Egil.RazorComponents.Testing.Library.SampleApp.Data;
using Microsoft.AspNetCore.Components;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Egil.RazorComponents.Testing.Library.SampleApp.CodeOnlyTests
{
    public class TodoItemTest : ComponentTestFixture
    {
        [Fact(DisplayName = "When no Todo is passed to item an exception is thrown")]
        public void Test001()
        {
            Should.Throw<ArgumentException>(() => RenderComponent<TodoItem>());
            Should.Throw<InvalidOperationException>(() => RenderComponent<TodoItem>((nameof(TodoItem.Todo), null)));
        }

        [Fact(DisplayName = "The control renders the expected output")]
        public void Test002()
        {
            var todo = new Todo { Id = 42, Text = "Hello world" };
            var cut = RenderComponent<TodoItem>((nameof(TodoItem.Todo), todo));

            cut.ShouldBe($@"<li id=""todo-{todo.Id}"" class=""list-group-item list-group-item-action"">
                                <span>{todo.Text}</span>
                                <span class=""float-right text-danger"">(click to complete)</span>
                            </li>");
        }

        [Fact(DisplayName = "When item or link is clicked, the OnCompleted event is raised")]
        public void Test003()
        {
            var todo = new Todo { Id = 42, Text = "Hello world" };
            var completedId = 0;
            var cut = RenderComponent<TodoItem>(
                (nameof(TodoItem.Todo), todo),
                EventCallback(nameof(TodoItem.OnCompleted), (int id) => completedId = id)
            );

            cut.Find("li").Click();

            completedId.ShouldBe(todo.Id);
        }
    }
}
