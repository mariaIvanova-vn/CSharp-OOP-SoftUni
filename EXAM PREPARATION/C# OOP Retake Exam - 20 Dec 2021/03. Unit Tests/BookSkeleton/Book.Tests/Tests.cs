namespace Book.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    public class Tests
    {
        [Test]
        public void BookThrowWithNullName()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book(null, "someAuthor");
            });
        }
        [Test]
        public void BookThrowWithEmptyName()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book("", "someAuthor");
            });
        }
        [Test]
        public void AuthorThrowWithNullName()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book("someBook", null);
            });
        }
        [Test]
        public void AuthorThrowWithEmptyName()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book("someBook", "");
            });
        }
        [Test]
        public void Currect()
        {
            Book book = new Book("someBook", "someAuthor");

            Assert.AreEqual("someAuthor", book.Author);
            Assert.AreEqual("someBook", book.BookName);
        }
        [Test]
        public void FindFootnoteMethodShouldReturnInformationAboutFootnote()
        {
            Book book = new Book("someBook", "someAuthor");
            book.AddFootnote(1, "blah blah");
            string footnote = book.FindFootnote(1);

            Assert.AreEqual("Footnote #1: blah blah", footnote);
        }
        [Test]
        public void AddThrow()
        {
            Book book = new Book("someBook", "someAuthor");
            book.AddFootnote(2, "something...");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.FindFootnote(7);
            });
            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AddFootnote(2, "something...");
            });
        }
        [Test]
        public void AlterFootnoteThrow()
        {
            Book book = new Book("someBook", "someAuthor");
            book.AddFootnote(2, "something...");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AlterFootnote(3, "alteredText");
            });
        }
        [Test]
        public void AlterFootnote()
        {
            Book book = new Book("someBook", "someAuthor");
            book.AddFootnote(1, "blah blah");
            book.AlterFootnote(1, "alteredText");

            Assert.AreEqual("Footnote #1: alteredText", book.FindFootnote(1));
        }
    }
}