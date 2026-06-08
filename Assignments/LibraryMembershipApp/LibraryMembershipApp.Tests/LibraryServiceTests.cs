using LibraryMembershipApp.Interfaces;
using LibraryMembershipApp.Models;
using LibraryMembershipApp.Services;
using Moq;
using NUnit.Framework;

namespace LibraryMembershipApp.Tests
{
    [TestFixture]
    public class LibraryServiceTests
    {
        private Mock<IMemberRepository> _memberRepoMock;
        private Mock<IBookRepository> _bookRepoMock;
        private Mock<INotificationService> _notificationMock;

        private LibraryService _libraryService;

        [SetUp]
        public void Setup()
        {
            _memberRepoMock = new Mock<IMemberRepository>();
            _bookRepoMock = new Mock<IBookRepository>();
            _notificationMock = new Mock<INotificationService>();

            _libraryService = new LibraryService(
                _memberRepoMock.Object,
                _bookRepoMock.Object,
                _notificationMock.Object);
        }

        [Test]
        public void BorrowBook_WhenAllConditionsAreValid_ShouldReturnSuccessMessage()
        {
            // Arrange

            var member = new Member
            {
                MemberId = 1,
                MemberName = "John",
                Email = "john@test.com",
                IsActive = true,
                BorrowedBookCount = 1
            };

            var book = new Book
            {
                BookId = 1,
                BookTitle = "C# Basics",
                IsAvailable = true
            };

            _memberRepoMock.Setup(x => x.GetMemberById(1))
                           .Returns(member);

            _bookRepoMock.Setup(x => x.GetBookById(1))
                         .Returns(book);

            // Act

            string result = _libraryService.BorrowBook(1, 1);

            // Assert

            Assert.That(result, Is.EqualTo("Book borrowed successfully"));

            _bookRepoMock.Verify(x => x.MarkBookAsBorrowed(1), Times.Once);

            _memberRepoMock.Verify(x => x.UpdateBorrowedBookCount(1), Times.Once);

            _notificationMock.Verify(
                x => x.SendBorrowNotification(
                    "john@test.com",
                    "C# Basics"),
                Times.Once);
        }

        [Test]
        public void BorrowBook_WhenMemberDoesNotExist_ShouldReturnMemberNotFound()
        {
            // Arrange

            _memberRepoMock.Setup(x => x.GetMemberById(1))
                           .Returns((Member)null);

            // Act

            string result = _libraryService.BorrowBook(1, 1);

            // Assert

            Assert.That(result, Is.EqualTo("Member not found"));

            _bookRepoMock.Verify(x => x.GetBookById(It.IsAny<int>()), Times.Never);
            _bookRepoMock.Verify(x => x.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
            _memberRepoMock.Verify(x => x.UpdateBorrowedBookCount(It.IsAny<int>()), Times.Never);
            _notificationMock.Verify(x => x.SendBorrowNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void BorrowBook_WhenMemberIsInactive_ShouldReturnMemberIsNotActive()
        {
            // Arrange

            _memberRepoMock.Setup(x => x.GetMemberById(1))
                           .Returns(new Member
                           {
                               IsActive = false
                           });

            // Act

            string result = _libraryService.BorrowBook(1, 1);

            // Assert

            Assert.That(result, Is.EqualTo("Member is not active"));
        }

        [Test]
        public void BorrowBook_WhenBookDoesNotExist_ShouldReturnBookNotFound()
        {
            // Arrange

            _memberRepoMock.Setup(x => x.GetMemberById(1))
                .Returns(new Member
                {
                    IsActive = true
                });

            _bookRepoMock.Setup(x => x.GetBookById(1))
                .Returns((Book)null);

            // Act

            string result = _libraryService.BorrowBook(1, 1);

            // Assert

            Assert.That(result, Is.EqualTo("Book not found"));
        }

        [Test]
        public void BorrowBook_WhenBookIsNotAvailable_ShouldReturnBookIsNotAvailable()
        {
            // Arrange

            _memberRepoMock.Setup(x => x.GetMemberById(1))
                .Returns(new Member
                {
                    IsActive = true
                });

            _bookRepoMock.Setup(x => x.GetBookById(1))
                .Returns(new Book
                {
                    IsAvailable = false
                });

            // Act

            string result = _libraryService.BorrowBook(1, 1);

            // Assert

            Assert.That(result, Is.EqualTo("Book is not available"));
        }

        [Test]
        public void BorrowBook_WhenBorrowingLimitReached_ShouldReturnBorrowingLimitReached()
        {
            // Arrange

            _memberRepoMock.Setup(x => x.GetMemberById(1))
                .Returns(new Member
                {
                    IsActive = true,
                    BorrowedBookCount = 3
                });

            _bookRepoMock.Setup(x => x.GetBookById(1))
                .Returns(new Book
                {
                    IsAvailable = true
                });

            // Act

            string result = _libraryService.BorrowBook(1, 1);

            // Assert

            Assert.That(result, Is.EqualTo("Borrowing limit reached"));
        }

        [Test]
        public void BorrowBook_WhenMemberIdIsInvalid_ShouldReturnInvalidMemberId()
        {
            // Arrange

            // Act

            string result = _libraryService.BorrowBook(0, 1);

            // Assert

            Assert.That(result, Is.EqualTo("Invalid member id"));
        }

        [Test]
        public void BorrowBook_WhenBookIdIsInvalid_ShouldReturnInvalidBookId()
        {
            // Arrange

            // Act

            string result = _libraryService.BorrowBook(1, 0);

            // Assert

            Assert.That(result, Is.EqualTo("Invalid book id"));
        }

        [Test]
        public void BorrowBook_WhenNormalMemberHasThreeBooks_ShouldReturnBorrowingLimitReached()
        {
            // Arrange

            _memberRepoMock.Setup(x => x.GetMemberById(1))
                .Returns(new Member
                {
                    IsActive = true,
                    BorrowedBookCount = 3,
                    IsPremiumMember = false
                });

            _bookRepoMock.Setup(x => x.GetBookById(1))
                .Returns(new Book
                {
                    IsAvailable = true
                });

            // Act

            string result = _libraryService.BorrowBook(1, 1);

            // Assert

            Assert.That(result, Is.EqualTo("Borrowing limit reached"));
        }

        [Test]
        public void BorrowBook_WhenPremiumMemberHasThreeBooks_ShouldAllowBorrowing()
        {
            // Arrange

            _memberRepoMock.Setup(x => x.GetMemberById(1))
                .Returns(new Member
                {
                    IsActive = true,
                    BorrowedBookCount = 3,
                    IsPremiumMember = true,
                    Email = "premium@test.com"
                });

            _bookRepoMock.Setup(x => x.GetBookById(1))
                .Returns(new Book
                {
                    IsAvailable = true,
                    BookTitle = "ASP.NET"
                });

            // Act

            string result = _libraryService.BorrowBook(1, 1);

            // Assert

            Assert.That(result, Is.EqualTo("Book borrowed successfully"));
        }

        [Test]
        public void BorrowBook_WhenPremiumMemberHasFiveBooks_ShouldReturnBorrowingLimitReached()
        {
            // Arrange

            _memberRepoMock.Setup(x => x.GetMemberById(1))
                .Returns(new Member
                {
                    IsActive = true,
                    BorrowedBookCount = 5,
                    IsPremiumMember = true
                });

            _bookRepoMock.Setup(x => x.GetBookById(1))
                .Returns(new Book
                {
                    IsAvailable = true
                });

            // Act

            string result = _libraryService.BorrowBook(1, 1);

            // Assert

            Assert.That(result, Is.EqualTo("Borrowing limit reached"));
        }

        [Test]
        public void BorrowBook_WhenAllConditionsAreValid_ShouldSendCorrectNotification()
        {
            // Arrange

            var member = new Member
            {
                Email = "john@test.com",
                IsActive = true,
                BorrowedBookCount = 1
            };

            var book = new Book
            {
                BookTitle = "Clean Code",
                IsAvailable = true
            };

            _memberRepoMock.Setup(x => x.GetMemberById(1))
                .Returns(member);

            _bookRepoMock.Setup(x => x.GetBookById(1))
                .Returns(book);

            // Act

            _libraryService.BorrowBook(1, 1);

            // Assert

            _notificationMock.Verify(
                x => x.SendBorrowNotification(
                    "john@test.com",
                    "Clean Code"),
                Times.Once);
        }
    }
}